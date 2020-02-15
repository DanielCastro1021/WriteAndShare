import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HeaderSidnaveComponent } from './header-sidnave.component';

describe('HeaderSidnaveComponent', () => {
  let component: HeaderSidnaveComponent;
  let fixture: ComponentFixture<HeaderSidnaveComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HeaderSidnaveComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HeaderSidnaveComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
