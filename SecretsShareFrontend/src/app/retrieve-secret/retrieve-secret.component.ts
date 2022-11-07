import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-retrieve-secret',
  templateUrl: './retrieve-secret.component.html',
  styleUrls: ['./retrieve-secret.component.css']
})
export class RetrieveSecretComponent implements OnInit {
  public secretRetrieved = true;
  public retrievedSecret:string = '';
  constructor() { }

  ngOnInit(): void {
  }
}
