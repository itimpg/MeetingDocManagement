/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { MoveContentComponent } from './move-content.component';

describe('MoveContentComponent', () => {
  let component: MoveContentComponent;
  let fixture: ComponentFixture<MoveContentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MoveContentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MoveContentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
