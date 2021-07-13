import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { StockComponent } from './stock/stock.component';
import { HomeComponent } from './home/home.component';
import { StockDetailsComponent } from './stock/stock-details/stock-details.component';

import { ApiAuthorizationModule } from 'src/api-authorization/api-authorization.module';
import { AuthorizeGuard } from 'src/api-authorization/authorize.guard';
import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';
import { ReactiveFormsModule } from '@angular/forms';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './material-module';
import { EventBusService } from './services/event-bus.service';
import { NumbersOnly } from './directives/numbersOnly';

const appRoutes: Routes = [
   { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'stock', component: StockComponent, canActivate: [AuthorizeGuard] },
]

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    StockComponent,
    StockDetailsComponent,
    NumbersOnly
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    MaterialModule,
    ApiAuthorizationModule,
    RouterModule.forRoot(appRoutes),
    BrowserAnimationsModule,
    ReactiveFormsModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true },
    EventBusService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
