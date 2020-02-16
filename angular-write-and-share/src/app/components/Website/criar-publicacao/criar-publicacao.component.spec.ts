import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CriarPublicacaoComponent } from './criar-publicacao.component';

describe('CriarPublicacaoComponent', () => {
  let component: CriarPublicacaoComponent;
  let fixture: ComponentFixture<CriarPublicacaoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CriarPublicacaoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CriarPublicacaoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
