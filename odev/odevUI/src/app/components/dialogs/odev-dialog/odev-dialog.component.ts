import { Component, Inject, OnInit } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Odev } from 'src/app/models/Odev';
import { Kategori } from 'src/app/models/Kategori';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-odev-dialog',
  templateUrl: './odev-dialog.component.html',
  styleUrls: ['./odev-dialog.component.css']
})
export class OdevDialogComponent implements OnInit {
  dialogBaslik:string;
  yeniKayit: Odev;
  islem: string;
  frm: UntypedFormGroup;
  kategoriler: Kategori[];
  jconfig: {};

  constructor(
    public dialogRef: MatDialogRef<OdevDialogComponent>,
    public frmBuild: UntypedFormBuilder,
    @Inject(MAT_DIALOG_DATA) public data: any,
    public apiServis: ApiService
  ) { 
    this.islem = data.islem;

    if(this.islem=="ekle"){
      this.dialogBaslik= "Ödev Ekle";
      this.yeniKayit= new Odev();
    }
    if(this.islem=="duzenle"){
      this.dialogBaslik= "Ödev Düzenle"
      this.yeniKayit= data.kayit;
    }
    if(this.islem=="detay"){
      this.dialogBaslik= "Ödev Detay"
      this.yeniKayit= data.kayit;
    }
    this.frm = this.FormOlustur();
  }

  ngOnInit() {
    this.KategoriListele();
  }
  FormOlustur(){
    return this.frmBuild.group({
      Baslik: (this.yeniKayit.Baslik),
      Icerik: (this.yeniKayit.Icerik),
      KategoriId: (this.yeniKayit.KategoriId)
    });
  }
  KategoriListele(){
    this.apiServis.KategoriListe().subscribe(d=>{
      this.kategoriler = d;
    });
  }
}
