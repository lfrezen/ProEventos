import { ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

import { Evento } from '@app/models/Evento';
import { EventoService } from '@app/services/evento.service';

@Component({
  selector: 'app-evento-detalhe',
  templateUrl: './evento-detalhe.component.html',
  styleUrls: ['./evento-detalhe.component.scss']
})
export class EventoDetalheComponent implements OnInit {

  evento = {} as Evento;
  form!: FormGroup;
  minimumDate!: Date;
  estadoSalvar = 'post';

  get f(): any {
    return this.form.controls;
  }

  get bsConfig(): any {
    return {
      adaptivePosition: true,
      dateInputFormat: 'DD/MM/YYYY hh:mm a',
      containerClass: 'theme-default',
      showWeekNumbers: false,
      minDate: this.minimumDate
    };
  }

  constructor(
    private fb: FormBuilder,
    private localeService: BsLocaleService,
    private router: ActivatedRoute,
    private eventoService: EventoService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService) {
    this.localeService.use('pt-br');
    this.minimumDate = new Date();
    this.minimumDate.setDate(this.minimumDate.getDate());
  }

  public carregarEvento(): void {
    const eventoIdParam = this.router.snapshot.paramMap.get('id');

    if (eventoIdParam !== null) {
      this.spinner.show();

      this.estadoSalvar = 'put';

      this.eventoService
        .obterEventoPorId(+eventoIdParam)
        .subscribe(
          (evento: Evento) => {
            this.evento = { ...evento };
            this.form.patchValue(this.evento);
          },
          (error: any) => {
            this.spinner.hide();
            this.toastr.error('Erro ao tentar carregar evento.', 'Erro');
            console.error(error);
          },
          () => this.spinner.hide()
        );
    }
  }

  ngOnInit(): void {
    this.carregarEvento();
    this.validation();
  }

  public validation(): void {
    this.form = this.fb.group({
      tema: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      local: ['', Validators.required],
      dataEvento: ['', Validators.required],
      quantidadePessoas: ['', [Validators.required, Validators.max(120000)]],
      telefone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      imagemUrl: ['', Validators.required]
    })
  }

  public resetForm(event: any): void {
    event.preventDefault();
    this.form.reset();
  }

  public cssValidator(campoForm: FormControl): any {
    return { 'is-invalid': campoForm.errors && campoForm.touched };
  }

  public salvarAlteracao(): void {
    this.spinner.show();
    if (this.form.valid) {

      if (this.estadoSalvar === 'post') {
        this.evento = { ...this.form.value }
        this.eventoService.postEvento(this.evento).subscribe(
          () => this.toastr.success('Evento atualizado com sucesso!', 'Atualizado'),
          (error: any) => {
            console.error(error);
            this.spinner.hide();
            this.toastr.error('Ocorreu um erro ao atualizar o evento', 'Erro');
          },
          () => this.spinner.hide()
        );
      } else {
        this.evento = { id: this.evento.id, ...this.form.value }
        this.eventoService.putEvento(this.evento.id, this.evento).subscribe(
          () => this.toastr.success('Evento atualizado com sucesso!', 'Atualizado'),
          (error: any) => {
            console.error(error);
            this.spinner.hide();
            this.toastr.error('Ocorreu um erro ao atualizar o evento', 'Erro');
          },
          () => this.spinner.hide()
        );
      }
    }
  }
}
