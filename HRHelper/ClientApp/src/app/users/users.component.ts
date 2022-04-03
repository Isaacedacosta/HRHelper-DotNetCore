import { Component, OnInit } from '@angular/core';
import { UserDataService } from '../_data-services/user.data-service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {

  users: any[] = [];
  user: any = {};
  showList: boolean = true;

  constructor(private userDataService: UserDataService) { }

  ngOnInit() {
    this.get();
  }

  get() {
    this.userDataService.get().subscribe((data: any[]) => {
      this.users = data;
      this.showList = true;
    }, error => {
      console.log(error);
      alert('System Error');
    })
  }

  post() {
    this.userDataService.post(this.user).subscribe(data => {
      if (data) {
        alert('User added to DB');
        this.user = { };
        this.get();
      } else {
        alert('Error');
      }
    }, error => {
      console.log(error);
      alert('System Error');
    })
  }


}
