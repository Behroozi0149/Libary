import { HttpClient } from '@angular/common/http';
import { Component, inject } from '@angular/core';

@Component({
  selector: 'app-borrows',
  standalone: true,
  imports: [],
  templateUrl: './borrows.component.html',
  styleUrl: './borrows.component.css'
})
export class BorrowsComponent {
  // http = inject(HttpClient)
  // data: any[] = [];
  // action='list';
  // ngOnInit(): void {
  //   console.log('Borrows init')
  //   this.http.get('http://localhost:5178/borrow/list').subscribe(res => {
  //     this.data = res as any[];
  //   });
  // }
  // addclick() {
  //   this.action='add';
  // }
}
