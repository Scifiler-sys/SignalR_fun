import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { NgChartsModule } from "ng2-charts";
import { HttpClientModule } from "@angular/common/http";

import { AppComponent } from './app.component';
import { PieComponent } from './pie/pie.component';

/*
  Reference the website link I provided in the website itself to understand what dependencies is needed to be supplied using NPM
*/

@NgModule({
  declarations: [
    AppComponent,
    PieComponent
  ],
  imports: [
    BrowserModule,
    NgChartsModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
