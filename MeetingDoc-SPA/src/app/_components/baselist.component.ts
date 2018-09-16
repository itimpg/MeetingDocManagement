import { BsModalRef, BsModalService } from 'ngx-bootstrap';
import { combineLatest, Subscription, Observable } from 'rxjs';
import { Pagination, PaginatedResult } from '../_models/pagination';
import { OnInit } from '@angular/core';
import { BaseService } from '../_services/base.service';
import { AlertifyService } from '../_services/alertify.service';
import { ActivatedRoute, Router } from '@angular/router';
import { BaseModel } from '../_models/BaseModel';
import { ShowModalParam } from '../_models/ShowModalParam';

export abstract class BaseListComponent<T extends BaseModel> implements OnInit {
  abstract itemName: string;
  abstract titleName: string;
  abstract actionName: string;

  bsModalRef: BsModalRef;
  items: T[];
  subscriptions: Subscription[] = [];
  pagination: Pagination;
  parentId: number;

  constructor(
    protected service: BaseService<T>,
    protected alertify: AlertifyService,
    protected modalService: BsModalService,
    protected route: ActivatedRoute,
    protected router: Router
  ) {}

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.items = data[this.actionName].result;
      this.pagination = data[this.actionName].pagination;
    });

    this.route.params.subscribe(params => {
      this.parentId = params['id'];
    });

    this.InitComponent();
  }

  InitComponent(): void {}

  loadItems() {
    const observableCollection = this.parentId
      ? this.service.getItemsByParent(
          this.parentId,
          this.pagination.currentPage,
          this.pagination.itemsPerPage
        )
      : this.service.getItems(
          this.pagination.currentPage,
          this.pagination.itemsPerPage
        );

    observableCollection.subscribe(
      (res: PaginatedResult<T[]>) => {
        this.items = res.result;
        this.pagination = res.pagination;
      },
      error => {
        this.alertify.error(error);
      }
    );
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadItems();
  }

  viewItem(item: T) {
    this.showItem(item.id, false);
  }

  abstract viewSubItem(item: T);

  addItem() {
    this.showItem(0, true);
  }

  editItem(item: T) {
    this.showItem(item.id, true);
  }

  deleteItem(item: T) {
    this.alertify.confirm(`Do you want to delet this ${this.itemName}?`, () => {
      this.service.delete(item.id).subscribe(
        () => {
          this.alertify.message('Delete success');
          this.loadItems();
        },
        error => {
          this.alertify.error(error);
        }
      );
    });
  }

  showItem(itemId: number, isEditable: boolean) {
    const combine = combineLatest(
      this.modalService.onShow,
      this.modalService.onShown,
      this.modalService.onHide,
      this.modalService.onHidden
    ).subscribe(() => {
      this.loadItems();
      this.unsubscribe();
    });

    this.subscriptions.push(combine);

    const param = new ShowModalParam();
    param.itemId = itemId;
    param.isEditable = isEditable;
    param.parentId = this.parentId;

    this.showModal(param);
  }

  abstract showModal(initialState: ShowModalParam): void;

  unsubscribe() {
    this.subscriptions.forEach((subscription: Subscription) => {
      subscription.unsubscribe();
    });
    this.subscriptions = [];
  }
}
