/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { MeetingAgendaListComponent } from './meeting-agenda-list.component';

describe('MeetingAgendaListComponent', () => {
  let component: MeetingAgendaListComponent;
  let fixture: ComponentFixture<MeetingAgendaListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MeetingAgendaListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MeetingAgendaListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
