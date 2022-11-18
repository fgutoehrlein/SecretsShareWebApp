import { Component, OnInit } from '@angular/core';
import { SecretDataModel } from 'src/app/models/secret-data-model';
import { SecretService } from 'src/app/services/secret.service';
import { v4 as uuid } from 'uuid';

@Component({
  selector: 'app-share-secret',
  templateUrl: './share-secret.component.html',
  styleUrls: ['./share-secret.component.css']
})
export class ShareSecretComponent implements OnInit {
  public secretSaved:boolean = false;
  public secretId:string = '';
  public password:string = '';

  constructor(private secretService:SecretService) { }

  ngOnInit(): void {
  }

  public saveSecret(inputData:SecretDataModel){

    this.password = inputData.Password;
    if (inputData.Password == '') {
      this.password = uuid();
    }

    let data = new SecretDataModel();
    data.SecretInput = inputData.SecretInput;
    data.Password = this.password;

    this.secretService.postSecret(data).subscribe(res => 
      {
        this.secretId = res;
      }
    );
    //this.secretId = res;

    if (this.secretId != '') {
      this.secretSaved = true;
    }
  }
}
