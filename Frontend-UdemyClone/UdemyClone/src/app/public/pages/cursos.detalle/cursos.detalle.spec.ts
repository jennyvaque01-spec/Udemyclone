import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CursosDetalle } from './cursos.detalle';

describe('CursosDetalle', () => {
  let component: CursosDetalle;
  let fixture: ComponentFixture<CursosDetalle>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CursosDetalle],
    }).compileComponents();

    fixture = TestBed.createComponent(CursosDetalle);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
