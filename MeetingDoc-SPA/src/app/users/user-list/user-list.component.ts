import { Component, OnInit } from '@angular/core';
import { BsModalRef, BsModalService, PaginationComponent } from 'ngx-bootstrap';
import { UsersService } from '../../_services/users.service';
import { UserComponent } from '../user/user.component';
import { AlertifyService } from '../../_services/alertify.service';
import { User } from '../../_models/User';
import { combineLatest, Subscription } from 'rxjs';
import { Pagination, PaginatedResult } from '../../_models/pagination';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {
  bsModalRef: BsModalRef;
  users: User[];
  subscriptions: Subscription[] = [];
  pagination: Pagination;

  constructor(
    private usersService: UsersService,
    private alertify: AlertifyService,
    private modalService: BsModalService,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.users = data.user.result;
      this.pagination = data.user.pagination;
    });
  }

  loadUsers() {
    this.usersService
      .getUsers(this.pagination.currentPage, this.pagination.itemsPerPage)
      .subscribe(
        (res: PaginatedResult<User[]>) => {
          this.users = res.result;
          this.pagination = res.pagination;
        },
        error => {
          this.alertify.error(error);
        }
      );
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadUsers();
  }

  viewUser(user: User) {
    this.showUser(user.id, false);
  }

  addUser() {
    this.showUser(0, true);
  }

  editUser(user: User) {
    this.showUser(user.id, true);
  }

  deleteUser(user: User) {
    this.alertify.confirm('Do you want to delet this user?', () => {
      this.usersService.delete(user.id).subscribe(
        () => {
          this.alertify.message('Delete success');
          this.loadUsers();
        },
        error => {
          this.alertify.error(error);
        }
      );
    });
  }

  showUser(userId: number, isEditable: boolean) {
    const combine = combineLatest(
      this.modalService.onShow,
      this.modalService.onShown,
      this.modalService.onHide,
      this.modalService.onHidden
    ).subscribe(() => {
      this.loadUsers();
      this.unsubscribe();
    });

    this.subscriptions.push(combine);

    this.bsModalRef = this.modalService.show(UserComponent);
    this.bsModalRef.content.setModel(userId, isEditable);
  }

  unsubscribe() {
    this.subscriptions.forEach((subscription: Subscription) => {
      subscription.unsubscribe();
    });
    this.subscriptions = [];
  }
}
