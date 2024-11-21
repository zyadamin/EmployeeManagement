import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ChangesPopUpComponent } from './changes-pop-up.component';

describe('ChangesPopUpComponent', () => {
  let component: ChangesPopUpComponent;
  let fixture: ComponentFixture<ChangesPopUpComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ChangesPopUpComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ChangesPopUpComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
