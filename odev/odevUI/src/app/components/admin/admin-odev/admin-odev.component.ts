import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialogRef, MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Odev } from 'src/app/models/Odev';
import { AlertService } from 'src/app/services/Myalert.service';
import { ApiService } from 'src/app/services/api.service';
import { ConfirmDialogComponent } from '../../dialogs/confirm-dialog/confirm-dialog.component';
import { OdevDialogComponent } from '../../dialogs/odev-dialog/odev-dialog.component';
import { Kategori } from 'src/app/models/Kategori';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-admin-odev',
  templateUrl: './admin-odev.component.html',
  styleUrls: ['./admin-odev.component.css']
})
export class AdminOdevComponent implements OnInit {
  odevler: Odev[];
  kategoriler: Kategori[];
  secKat: Kategori;
  katId: number;
  UyeId: number;
  dataSource: any;
  displayedColumns = ['Baslik', 'Tarih','UyeKadi','Goruntulenme','detay'];
  @ViewChild(MatSort) sort:MatSort
  @ViewChild(MatPaginator) paginator:MatPaginator
  dialogRef:MatDialogRef<OdevDialogComponent>;
  dialogaRefConfirm: MatDialogRef<ConfirmDialogComponent>;

  constructor(
    public apiServis: ApiService,
    public matDialog : MatDialog,
    public alert : AlertService,
    public route : ActivatedRoute
  ) { }

  ngOnInit() {
    this.KategoriListele();
    this.UyeId=parseInt(localStorage.getItem("uid"));
    this.route.params.subscribe(p=>{
      if(p['katId']){
      this.katId = p['katId'];
      this.KategoriById();
      }
    });
  }
  KategoriById(){
    this.apiServis.KategoriById(this.katId).subscribe((d:Kategori) =>{
      this.secKat = d;
      this.OdevListele();
    });
  }

  OdevListele(){
    this.apiServis.OdevListeByKatId(this.katId).subscribe(d=>{
      this.odevler = d;
      this.dataSource = new MatTableDataSource(this.odevler);
      this.dataSource.sort = this.sort;
      this.dataSource.paginator = this.paginator;
    });
  }
  
  KategoriListele(){
    this.apiServis.KategoriListe().subscribe(d=>{
      this.kategoriler = d;
    });
  }
  KategoriSec(katId:number){
    this.katId = katId;
    this.OdevListele();
  }

  Ekle(){
    var yeniKayit: Odev = new Odev();
    this.dialogRef= this.matDialog.open(OdevDialogComponent,{
      width: '800px',
      data:{
        kayit:yeniKayit,
        islem: 'ekle'
      }
    });
    this.dialogRef.afterClosed().subscribe(d => {
    if(d){
      yeniKayit= d;
      yeniKayit.Foto = "foto.jpg";
      yeniKayit.Tarih = new Date();
      yeniKayit.Goruntulenme= 0;
      yeniKayit.UyeId=this.UyeId;
      this.apiServis.OdevEkle(yeniKayit).subscribe((s:any)=>{
        this.alert.AlertUygula(s);
        if(s.islem){
          this.OdevListele();
        }
      });
    }
    });
  }

  Duzenle(kayit:Odev){
    this.dialogRef= this.matDialog.open(OdevDialogComponent,{
      width: '800px',
      data:{
        kayit:kayit,
        islem: 'duzenle'
      }
    });
    this.dialogRef.afterClosed().subscribe(d => {
    if(d){
      kayit.KategoriAdi =d.KategoriAdi;
      this.apiServis.OdevDuzenle(kayit).subscribe((s:any)=>{
        this.alert.AlertUygula(s);
        if(s.islem){
          this.OdevListele();
        }
      });
    }
    });
  }

  Detay(kayit:Odev){
    this.dialogRef= this.matDialog.open(OdevDialogComponent,{
      width: '800px',
      data:{
        kayit:kayit,
        islem: 'detay'
      }
    });
  }

  Sil(kayit:Odev){
    this.dialogaRefConfirm =this.matDialog.open(ConfirmDialogComponent,{
      width: '400px',
    });
    this.dialogaRefConfirm.componentInstance.dialogMesaj=kayit.Baslik + " Başlıklı ödev silinecektir onaylıyor musunuz?";
    this.dialogaRefConfirm.afterClosed().subscribe(d=>{
      if (d){
        this.apiServis.OdevSil(kayit.OdevId).subscribe((s:any)=>{
          this.alert.AlertUygula(s);
          if(s.islem){
            this.OdevListele();
          }
        });
      }
    });
  }
}

