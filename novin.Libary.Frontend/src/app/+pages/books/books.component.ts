import { DecimalPipe } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Title } from '@angular/platform-browser';


@Component({
  selector: 'app-books',
  standalone: true,
  imports: [FormsModule,DecimalPipe],
  templateUrl: './books.component.html',
  styleUrl: './books.component.css'
})
export class BooksComponent implements OnInit {
  http = inject(HttpClient)
  data: any[] = [];
  action = 'list';
  entity = {
    title: '',
    writer: '',
    price: 0
  }
  ngOnInit(): void {
    console.log('Books init')
    this.http.get('http://localhost:5178/books/list').subscribe(res => {
      this.data = res as any[];
    });
  }
  addclick() {
    this.action = 'add';
  }
  cancel() {
    this.action = 'list';
  }
  refresh() {
    this.http.get('http://localhost:5178/books/list').subscribe(res => {
      this.data = res as any[];
    })
  }
  editclick(item: any) {
    this.action = 'edit';
    this.entity = item;
  }
  deleteclick(_t12: any) {
    throw new Error('Method not implemented.');
  }
  ok() {
    if (this.action == 'add') {
      this.http.post('http://localhost:5178/books/add', this.entity).subscribe(res => {
        this.action = 'list';
        this.refresh();
      });
    }
  }
}
