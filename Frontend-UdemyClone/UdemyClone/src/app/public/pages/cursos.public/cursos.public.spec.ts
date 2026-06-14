import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CursosPublic } from './cursos.public';

describe('CursosPublic', () => {
  let component: CursosPublic;
  let fixture: ComponentFixture<CursosPublic>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CursosPublic],
    }).compileComponents();

    fixture = TestBed.createComponent(CursosPublic);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
