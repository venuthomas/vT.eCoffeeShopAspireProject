/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { AdminPanelService } from './admin-panel.service';

describe('Service: AdminPanel', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AdminPanelService]
    });
  });

  it('should ...', inject([AdminPanelService], (service: AdminPanelService) => {
    expect(service).toBeTruthy();
  }));
});
