import { BsModalRef, BsModalService } from 'ngx-bootstrap';
import { combineLatest, Subscription } from 'rxjs';
import { Pagination, PaginatedResult } from '../_models/pagination';
import { OnInit } from '@angular/core';
import { BaseService } from '../_services/base.service';
import { AlertifyService } from '../_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { BaseModel } from '../_models/BaseModel';

export abstract class BaseListComponent<T extends BaseModel> implements OnInit {
  abstract itemName: string;
  abstract titleName: string;
  abstract actionName: string;

  bsModalRef: BsModalRef;
  items: T[];
  subscriptions: Subscription[] = [];
  pagination: Pagination;

  constructor(
    protected service: BaseService<T>,
    protected alertify: AlertifyService,
    protected modalService: BsModalService,
    protected route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.items = data[this.actionName].result;
      this.pagination = data[this.actionName].pagination;
    });
  }

  loadItems() {
    this.service
      .getItems(this.pagination.currentPage, this.pagination.itemsPerPage)
      .subscribe(
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

    this.showModal(itemId, isEditable);
  }

  abstract showModal(itemId: number, isEditable: boolean): void;

  unsubscribe() {
    this.subscriptions.forEach((subscription: Subscription) => {
      subscription.unsubscribe();
    });
    this.subscriptions = [];
  }
}
