import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-share-secret',
  templateUrl: './share-secret.component.html',
  styleUrls: ['./share-secret.component.css']
})
export class ShareSecretComponent implements OnInit {
  public secretSaved:boolean = false;
  public secretId:string = '';
  public password:string = '';

  constructor() { }

  ngOnInit(): void {
  }

  public saveSecret(){
    this.secretSaved = true;
  }
}
