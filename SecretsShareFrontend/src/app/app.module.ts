import { NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavigationBarComponent } from './navigation-bar/navigation-bar.component';
import { ShareSecretComponent } from './share-secret/share-secret.component';
import { RetrieveSecretComponent } from './retrieve-secret/retrieve-secret.component';
import { AboutComponent } from './about/about.component';

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
    RouterModule.forRoot([
      { path: '', component: ShareSecretComponent },
      { path: 'retrieve', component: RetrieveSecretComponent },
      { path: 'about', component: AboutComponent },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule { }
