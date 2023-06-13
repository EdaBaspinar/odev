import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Odev } from 'src/app/models/Odev';
import { Sonuc } from 'src/app/models/Sonuc';
import { Yorum } from 'src/app/models/Yorum';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-odev',
  templateUrl: './odev.component.html',
  styleUrls: ['./odev.component.css']
})
export class OdevComponent implements OnInit {
  OdevId:number;
  odev: Odev;
  yorumlar: Yorum[];
  constructor(
    public apiServis: ApiService,
    public route: ActivatedRoute
  ) { }

  ngOnInit() {
    this.route.params.subscribe(p=>{
      if(p['OdevId']){
        this.OdevId=p['OdevId'];
        this.OdevById();
        this.OdevYorumListe();
      }
    });
  }
  OdevById(){
    this.apiServis.OdevById(this.OdevId).subscribe((d:Odev)=>{
      this.odev = d;
      this.OdevOkunduYap();
    });
  }
  OdevOkunduYap(){
    this.odev.Goruntulenme += 1;
    this.apiServis.OdevDuzenle(this.odev).subscribe();
  }

  OdevYorumListe() {
    this.apiServis.YorumListeByodevId(this.OdevId).subscribe((d:Yorum[])=>{
      this.yorumlar = d ;
    });
  }

  YorumEkle(yorumMetni:string){
    var yorum:Yorum = new Yorum();
    var uyeId: number = parseInt(localStorage.getItem("uid"));
    yorum.OdevId=this.OdevId;
    yorum.UyeId = uyeId;
    yorum.YorumIcerik = yorumMetni;
    yorum.Tarih = new Date();

    this.apiServis.YorumEkle(yorum).subscribe((d:Sonuc) => {
      if(d.islem) {
        this.OdevYorumListe();
      }
    });

  }

}