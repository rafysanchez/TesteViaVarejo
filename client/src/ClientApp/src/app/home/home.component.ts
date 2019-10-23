import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {

  public amigos: Amigo[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Amigo[]>(baseUrl + 'api/Demo/ObterAmigos').subscribe(result => {
      this.amigos = result;
    }, error => console.error(error));
  }
}
interface Amigo {
  amigoId: number;
  nome: string;
  distancia: number;
}
