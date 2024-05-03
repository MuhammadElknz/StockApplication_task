import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CommuncationService {
  constructor() {}

  private subjectClose = new BehaviorSubject(1);

  sendClose(response: any) {
    this.subjectClose.next(response);
  }

  getClose(): Observable<any> {
    return this.subjectClose.asObservable();
  }
}
