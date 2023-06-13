import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Odev } from 'src/app/models/Odev';
import { Uye } from 'src/app/models/Uye';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-uyeodev',
  templateUrl: './uyeodev.component.html',
  styleUrls: ['./uyeodev.component.css']
})
export class UyeodevComponent implements OnInit {
  odevler: Odev[];
  UyeId:  number;
  uye: Uye;
  constructor(
    public apiServis: ApiService,
    public route: ActivatedRoute
  ) { }

  ngOnInit() {
    this.route.params.subscribe(p=>{
      if(p['UyeId']){
        this.UyeId=p['UyeId'];
        this.UyeById();
        this.OdevListeByUyeId();
      }
    });
  }
  UyeById(){
    this.apiServis.UyeById(this.UyeId).subscribe((d:Uye)=>{
      this.uye= d;
    });
  }
  OdevListeByUyeId(){
    this.apiServis.OdevListeByUyeId(this.UyeId).subscribe((d:Odev[]) =>{
      this.odevler = d;
    });    
  }

}
