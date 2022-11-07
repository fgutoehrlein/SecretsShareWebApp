import { NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { MaterialCollectionModule } from '../material.module';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavigationBarComponent } from './navigation-bar/navigation-bar.component';
import { ShareSecretComponent } from './share-secret/share-secret.component';
import { RetrieveSecretComponent } from './retrieve-secret/retrieve-secret.component';
import { AboutComponent } from './about/about.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
  declarations: [
    AppComponent,
    NavigationBarComponent,
    ShareSecretComponent,
    RetrieveSecretComponent,
    AboutComponent
  ],
  imports: [
    NgbModule,
    BrowserModule,
    MaterialCollectionModule,
    RouterModule.forRoot([
      { path: '', component: ShareSecretComponent },
      { path: 'retrieve', component: RetrieveSecretComponent },
      { path: 'about', component: AboutComponent },
    ]),
    BrowserAnimationsModule
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule { }
