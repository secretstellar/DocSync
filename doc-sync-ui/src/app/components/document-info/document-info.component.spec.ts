import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocumentInfoComponent } from './document-info.component';

describe('DocumentInfoComponent', () => {
  let component: DocumentInfoComponent;
  let fixture: ComponentFixture<DocumentInfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DocumentInfoComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DocumentInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
