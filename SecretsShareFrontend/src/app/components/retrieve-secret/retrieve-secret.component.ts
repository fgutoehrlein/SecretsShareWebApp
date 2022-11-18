import { Component, OnInit } from '@angular/core';
import { SecretService } from 'src/app/services/secret.service';

@Component({
  selector: 'app-retrieve-secret',
  templateUrl: './retrieve-secret.component.html',
  styleUrls: ['./retrieve-secret.component.css']
})
export class RetrieveSecretComponent implements OnInit {
  public secretRetrieved = false;
  public retrievedSecret:string = '';

  constructor(private secretService:SecretService) { }

  ngOnInit(): void {
  }

  public getSecret(inputData:any){
    this.secretService.getSecret(inputData.SecretId,inputData.Password).subscribe(res => 
      {
        this.retrievedSecret = res;

        if (this.retrievedSecret != '') {
          this.secretRetrieved = true;
        }
      }
    );
  }
}
