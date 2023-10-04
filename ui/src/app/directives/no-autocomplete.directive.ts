import { Directive, ElementRef, OnInit, Renderer2 } from '@angular/core';

@Directive({
  selector: 'input', // seleciona todos os elementos <input>
})
export class NoAutocompleteDirective implements OnInit {
  constructor(private el: ElementRef, private renderer: Renderer2) {}

  ngOnInit() {
    this.renderer.setAttribute(this.el.nativeElement, 'autocomplete', 'off');
  }
}
