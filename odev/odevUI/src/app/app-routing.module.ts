import { CanActivate } from '@angular/router';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { AdminKategoriComponent } from './components/admin/admin-kategori/admin-kategori.component';
import { AdminUyeComponent } from './components/admin/admin-uye/admin-uye.component';
import { AdminOdevComponent } from './components/admin/admin-odev/admin-odev.component';
import { AuthGuard } from './services/AuthGuard';
import { AdminComponent } from './components/admin/admin/admin.component';
import { OdevComponent } from './components/odev/odev.component';
import { KategoriComponent } from './components/Kategori/Kategori.component';
import { UyeodevComponent } from './components/uyeodev/uyeodev.component';

const routes: Routes = [
  {
    path: '', component: HomeComponent
  },

  {
    path: 'login', component: LoginComponent
  },

  {
    path: 'odev/:OdevId', component: OdevComponent
  },

  {
    path: 'kategori/:katId', component: KategoriComponent
  },

  {
    path: 'uyeodev/:UyeId', component: UyeodevComponent
  },

  {
    path: 'admin', component: AdminComponent,
    canActivate: [AuthGuard],
    data:{
      yetkiler:['Admin'],
      gerigit: '/login'
    }
  },

  {
    path: 'admin/kategori', component: AdminKategoriComponent,
    canActivate: [AuthGuard],
    data:{
      yetkiler:['Admin'],
      gerigit: '/login'
    }
  },

  {
    path: 'admin/uye', component: AdminUyeComponent
  },

  {
    path: 'admin/odev', component: AdminOdevComponent
  },
  
  {
    path: 'admin/odev/:katId', component: AdminOdevComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
