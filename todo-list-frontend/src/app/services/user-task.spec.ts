import { TestBed } from '@angular/core/testing';

import { UserTask } from './user-task';

describe('UserTask', () => {
  let service: UserTask;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(UserTask);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
