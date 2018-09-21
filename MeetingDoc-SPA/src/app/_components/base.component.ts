import { OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap';
import { BaseModel } from '../_models/BaseModel';
import { BaseService } from '../_services/base.service';
import { AlertifyService } from '../_services/alertify.service';

export abstract class BaseComponent<T extends BaseModel> implements OnInit {
  protected abstract action: string;

  itemId: number;
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

  ngOnInit() {
    this.InitComponent();
  }

  InitComponent() {
    if (this.itemId === 0) {
      this.isEditable = true;
      this.title = `เพิ่มข้อมูล ${this.action}`;
      this.initAdd();
    } else {
      this.title = this.isEditable
        ? `แก้ไข ${this.action}`
        : `ดูรายละเอียด ${this.action}`;
      this.service.getItem(this.itemId).subscribe(
        result => {
          this.model = this.ConvertResultToModel(result);
          this.initEdit();
        },
        error => {
          this.alertify.error(error);
        }
      );
    }
  }

  initAdd() {}
  initEdit() {}

  ConvertResultToModel(result: any): T {
    return result;
  }

  PrepareBeforeSave(): T {
    return this.model;
  }

  saveItem() {
    const model = this.PrepareBeforeSave();

    if (model.id === 0) {
      this.service.add(model).subscribe(
        success => {
          this.alertify.message('บันทึกสำเร็จ');
          this.bsModalRef.hide();
        },
        error => {
          this.alertify.error(error);
        }
      );
    } else {
      this.service.edit(model).subscribe(
        success => {
          this.alertify.message('บันทึกสำเร็จ');
          this.bsModalRef.hide();
        },
        error => {
          this.alertify.error(error);
        }
      );
    }
  }
}
