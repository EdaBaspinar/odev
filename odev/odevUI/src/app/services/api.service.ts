import { Observable } from 'rxjs';
import { Odev } from '../models/Odev';
import { Yorum } from './../models/Yorum';
import { Uye } from './../models/Uye';
import { Kategori } from './../models/Kategori';

import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Sonuc } from '../models/Sonuc';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  apiUrl = "http://localhost:37567/api/";

  constructor(
    public http: HttpClient
  ) { }

  /*   Oturum İşlemleri Başla  */
  tokenAl(kadi: string, parola: string) {
    var data = "username=" + kadi + "&password=" + parola + "&grant_type=password";
    var reqHeader = new HttpHeaders({ "Content-Type": "application/x-www-form-urlencoded" });
    return this.http.post(this.apiUrl + "/token", data, { headers: reqHeader });
  }
  oturumKontrol() {
    if (localStorage.getItem("token")) {
      return true;
    }
    else {
      return false;
    }
  }

  yetkiKontrol(yetkiler: any[]) {
    var sonuc: boolean = false;

    var uyeYetkiler: string[] = JSON.parse(localStorage.getItem("uyeYetkileri"));

    if (uyeYetkiler) {
      yetkiler.forEach(element => {
        if (uyeYetkiler.indexOf(element) > -1) {
          sonuc = true;
        }
      });
    }

    return sonuc;
  }

  /*   Oturum İşlemleri Bitiş  */


  /*  API  */

  KategoriListe(): Observable<Kategori[]> {
    return this.http.get<Kategori[]>(this.apiUrl + "/kategoriliste");
  }
  KategoriById(katId: number): Observable<Kategori> {
    return this.http.get<Kategori>(this.apiUrl + "/kategoribyid/" + katId);
  }
  KategoriEkle(kat: Kategori): Observable<any> {
    return this.http.post<Sonuc>(this.apiUrl + "/kategoriekle", kat);
  }
  KategoriDuzenle(kat: Kategori): Observable<any> {
    return this.http.put<Sonuc>(this.apiUrl + "/kategoriduzenle", kat);
  }
  KategoriSil(katId: number): Observable<any> {
    return this.http.delete<Sonuc>(this.apiUrl + "/kategorisil/" + katId);
  }

  OdevListe(): Observable<Odev[]> {
    return this.http.get<Odev[]>(this.apiUrl + "/odevliste");
  }
  OdevListeSonEklenenler(s: number): Observable<Odev[]> {
    return this.http.get<Odev[]>(this.apiUrl + "/odevlistesoneklenenler/" + s);
  }
  OdevListeByKatId(katId: number): Observable<Odev[]> {
    return this.http.get<Odev[]>(this.apiUrl + "/odevlistebykatid/" + katId);
  }
  OdevListeByUyeId(uyeId: number): Observable<Odev[]> {
    return this.http.get<Odev[]>(this.apiUrl + "/odevlistebyuyeid/" + uyeId);
  }
  OdevById(OdevId: number): Observable<Odev> {
    return this.http.get<Odev>(this.apiUrl + "/odevbyid/" + OdevId);
  }
  OdevEkle(odev: Odev): Observable<any> {
    return this.http.post<Sonuc>(this.apiUrl + "/odevekle", odev);
  }
  OdevDuzenle(odev: Odev): Observable<any> {
    return this.http.put<Sonuc>(this.apiUrl + "/odevduzenle", odev);
  }
  OdevSil(OdevId: number): Observable<any> {
    return this.http.delete<Sonuc>(this.apiUrl + "/odevsil/" + OdevId);
  }


  UyeListe(): Observable<Uye[]> {
    return this.http.get<Uye[]>(this.apiUrl + "/uyeliste");
  }
  UyeById(uyeId: number): Observable<Uye> {
    return this.http.get<Uye>(this.apiUrl + "/uyebyid/" + uyeId);
  }
  UyeEkle(uye: Uye): Observable<any> {
    return this.http.post<Sonuc>(this.apiUrl + "/uyeekle", uye);
  }
  UyeDuzenle(uye: Uye): Observable<any> {
    return this.http.put<Sonuc>(this.apiUrl + "/uyeduzenle", uye);
  }
  UyeSil(uyeId: number): Observable<any> {
    return this.http.delete<Sonuc>(this.apiUrl + "/uyesil/" + uyeId);
  }

  YorumListe(): Observable<Yorum[]> {
    return this.http.get<Yorum[]>(this.apiUrl + "/yorumliste");
  }
  YorumListeByUyeId(uyeId: number): Observable<Yorum[]> {
    return this.http.get<Yorum[]>(this.apiUrl + "/yorumlistebyuyeid/" + uyeId);
  }
  YorumListeByodevId(OdevId: number): Observable<Yorum[]> {
    return this.http.get<Yorum[]>(this.apiUrl + "/yorumlistesoneklenenler/" + OdevId);
  }
  YorumListeSonEklenenler(s: number): Observable<Yorum[]> {
    return this.http.get<Yorum[]>(this.apiUrl + "/yorumliste/" + s);
  }
  YorumById(yorumId: number): Observable<Yorum> {
    return this.http.get<Yorum>(this.apiUrl + "/yorumbyid/" + yorumId);
  }
  YorumEkle(yorum: Yorum): Observable<any> {
    return this.http.post<Sonuc>(this.apiUrl + "/yorumekle", yorum);
  }
  YorumDuzenle(yorum: Yorum): Observable<any> {
    return this.http.put<Sonuc>(this.apiUrl + "/yorumduzenle", yorum);
  }
  YorumSil(yorumId: number): Observable<any> {
    return this.http.delete<Sonuc>(this.apiUrl + "/yorumsil/" + yorumId);
  }
}
