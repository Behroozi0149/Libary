import { HttpClient } from '@angular/common/http';
import { Component, inject } from '@angular/core';

@Component({
  selector: 'app-members',
  standalone: true,
  imports: [],
  templateUrl: './members.component.html',
  styleUrl: './members.component.css'
})
export class MembersComponent {
  // http = inject(HttpClient)
  // data: any[] = [];
  // action='list';
  // ngOnInit(): void {
  //   console.log('Books init')
  //   this.http.get('http://localhost:5178/members/list').subscribe(res => {
  //     this.data = res as any[];
  //   });
  // }
  // addclick() {
  //   this.action='add';
  // }

}
