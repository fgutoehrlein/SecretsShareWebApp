import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShareSecretComponent } from './share-secret.component';

describe('ShareSecretComponent', () => {
  let component: ShareSecretComponent;
  let fixture: ComponentFixture<ShareSecretComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShareSecretComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ShareSecretComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
