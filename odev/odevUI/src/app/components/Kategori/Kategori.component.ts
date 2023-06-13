import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Kategori } from 'src/app/models/Kategori';
import { Odev } from 'src/app/models/Odev';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-Kategori',
  templateUrl: './Kategori.component.html',
  styleUrls: ['./Kategori.component.css']
})
export class KategoriComponent implements OnInit {
  odevler: Odev[];
  katId:  number;
  kat:Kategori;
  constructor(
    public apiServis: ApiService,
    public route: ActivatedRoute
  ) { }

  ngOnInit() {
    this.route.params.subscribe(p=>{
      if(p['katId']){
        this.katId=p['katId'];
        this.KategoriById();
        this.OdevListeByKatId();
      }
    });
  }
  KategoriById(){
    this.apiServis.KategoriById(this.katId).subscribe((d:Kategori)=>{
      this.kat= d;
    });
  }
  OdevListeByKatId(){
    this.apiServis.OdevListeByKatId(this.katId).subscribe((d:Odev[]) =>{
      this.odevler = d;
    });    
  }

}
