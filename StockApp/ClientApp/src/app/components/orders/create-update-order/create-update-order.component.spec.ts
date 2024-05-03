import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateUpdateOrderComponent } from './create-update-order.component';

describe('CreateUpdateOrderComponent', () => {
  let component: CreateUpdateOrderComponent;
  let fixture: ComponentFixture<CreateUpdateOrderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateUpdateOrderComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreateUpdateOrderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
