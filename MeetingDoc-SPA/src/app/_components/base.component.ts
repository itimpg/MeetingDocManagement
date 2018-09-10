import { OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap';
import { BaseModel } from '../_models/BaseModel';
import { BaseService } from '../_services/base.service';
import { AlertifyService } from '../_services/alertify.service';

export abstract class BaseComponent<T extends BaseModel> implements OnInit {
  protected abstract action: string;

  model: T;
  title: string;
  isEditable: boolean;
  parentId: number;

  constructor(
    public bsModalRef: BsModalRef,
    protected service: BaseService<T>,
    protected alertify: AlertifyService
  ) {
    this.model = {} as T;
    this.model.id = 0;
  }

  ngOnInit() {}

  setModel(itemId: number, isEditable: boolean, parentId?) {
    this.parentId = parentId;

    if (itemId === 0) {
      this.isEditable = true;
      this.title = `Add ${this.action}`;
    } else {
      this.isEditable = isEditable;
      this.title = this.isEditable
        ? `Edit  ${this.action}`
        : `View ${this.action}`;
      this.service.getItem(itemId).subscribe(
        result => {
          this.model = result;
        },
        error => {
          this.alertify.error(error);
        }
      );
    }
  }

  PrepareBeforeSave(): T {
    return this.model;
  }

  saveItem() {
    const model = this.PrepareBeforeSave();

    if (model.id === 0) {
      this.service.add(model).subscribe(
        success => {
          this.alertify.message('save success');
          this.bsModalRef.hide();
        },
        error => {
          this.alertify.error(error);
        }
      );
    } else {
      this.service.edit(model).subscribe(
        success => {
          this.alertify.message('save success');
          this.bsModalRef.hide();
        },
        error => {
          this.alertify.error(error);
        }
      );
    }
  }
}
