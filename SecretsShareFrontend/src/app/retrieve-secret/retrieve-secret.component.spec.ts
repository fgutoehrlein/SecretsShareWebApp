import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RetrieveSecretComponent } from './retrieve-secret.component';

describe('RetrieveSecretComponent', () => {
  let component: RetrieveSecretComponent;
  let fixture: ComponentFixture<RetrieveSecretComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RetrieveSecretComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RetrieveSecretComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
