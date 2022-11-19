import { NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { MaterialCollectionModule } from '../material.module';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { NavigationBarComponent } from './components/navigation-bar/navigation-bar.component';
import { ShareSecretComponent } from './components/share-secret/share-secret.component';
import { RetrieveSecretComponent } from './components/retrieve-secret/retrieve-secret.component';
import { AboutComponent } from './components/about/about.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { SecretService } from './services/secret.service';

@NgModule({
  declarations: [
    AppComponent,
    NavigationBarComponent,
    ShareSecretComponent,
    RetrieveSecretComponent,
    AboutComponent
  ],
  imports: [
    FormsModule,
    NgbModule,
    BrowserModule,
    MaterialCollectionModule,
    HttpClientModule,
    RouterModule.forRoot([
      { path: '', component: ShareSecretComponent },
      { path: 'retrieve', component: RetrieveSecretComponent },
      { path: 'about', component: AboutComponent },
    ],
      { useHash: true }
    ),
    BrowserAnimationsModule
  ],
  providers: [SecretService],
  bootstrap: [AppComponent],
})
export class AppModule { }
