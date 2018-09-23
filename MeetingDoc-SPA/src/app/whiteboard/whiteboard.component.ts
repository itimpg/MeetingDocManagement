import { Component, ViewEncapsulation, Input, OnInit } from '@angular/core';
import {
  CanvasWhiteboardUpdate,
  CanvasWhiteboardOptions
} from 'ng2-canvas-whiteboard';
import { AlertifyService } from '../_services/alertify.service';
import { MeetingNoteService } from '../_services/meeting-note.service';
import { MeetingNote } from '../_models/meeting-note';
import { debug } from 'util';

@Component({
  selector: 'app-whiteboard',
  templateUrl: './whiteboard.component.html',
  styleUrls: ['./whiteboard.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class WhiteboardComponent implements OnInit {
  @Input()
  image: string;
  @Input()
  contentId: number;
  @Input()
  ratio: number;

  canvasOptions: CanvasWhiteboardOptions = {
    drawButtonEnabled: false,
    drawButtonClass: 'fa fa-pencil',
    drawButtonText: 'Note',
    clearButtonEnabled: true,
    clearButtonClass: 'fas fa-eraser',
    clearButtonText: 'Clear',
    undoButtonText: 'Undo',
    undoButtonClass: 'fas fa-undo',
    undoButtonEnabled: true,
    redoButtonText: 'Redo',
    redoButtonClass: 'fas fa-redo',
    redoButtonEnabled: true,
    colorPickerEnabled: true,
    saveDataButtonEnabled: true,
    saveDataButtonClass: 'fas fa-save',
    saveDataButtonText: 'Save',
    lineWidth: 2,
    strokeColor: 'rgb(0,0,0)',
    shouldDownloadDrawing: false,
    drawingEnabled: true,
    imageUrl: this.image,
    aspectRatio: this.ratio
  };

  constructor(
    protected noteService: MeetingNoteService,
    protected alertify: AlertifyService
  ) {}

  ngOnInit(): void {
    this.canvasOptions.aspectRatio = this.ratio;
  }

  sendBatchUpdate(updates: CanvasWhiteboardUpdate[]) {}
  onCanvasClear() {}
  onCanvasUndo(updateUUID: string) {}
  onCanvasRedo(updateUUID: string) {}
  onCanvasSave(updateUUID: string) {
    const note = new MeetingNote();
    note.note = updateUUID;
    note.meetingContentId = this.contentId;
    this.noteService.add(note).subscribe(
      success => {
        this.alertify.message('บันทึกสำเร็จ');
      },
      error => {
        this.alertify.error(error);
      }
    );
  }
}
