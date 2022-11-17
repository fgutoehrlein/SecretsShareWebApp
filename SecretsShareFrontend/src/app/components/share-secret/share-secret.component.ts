import { Component, OnInit } from '@angular/core';
import { SecretDataModel } from 'src/app/models/secret-data-model';

@Component({
  selector: 'app-share-secret',
  templateUrl: './share-secret.component.html',
  styleUrls: ['./share-secret.component.css']
})
export class ShareSecretComponent implements OnInit {
  public secretSaved:boolean = false;
  public secretId:string = '';
  public secretString:string = '';
  public password:string = '';

  constructor() { }

  ngOnInit(): void {
  }

  public saveSecret(inputData:any){
    // if (this.password == '') {
    //   this.password = uuidv4();
    // }

    let data = new SecretDataModel();
    data.SecretInput = this.secretString;
    data.Password = this.password;
    this.secretSaved = true;
  }
}
