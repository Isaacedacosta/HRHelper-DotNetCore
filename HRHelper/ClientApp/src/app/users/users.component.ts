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
      this.user = {};
      this.showList = true;
    }, error => {
      console.log(error);
      alert('System Error');
    })
  }

  save() {
    if (this.user.id) {
      this.put();
    } else {
      this.post();
    }
  }

  
  post() {
    this.userDataService.post(this.user).subscribe(data => {
      if (data) {
        alert('User added to DB');
        this.user = {};
        this.get();
      } else {
        alert('Error');
      }
    }, error => {
      console.log(error);
      alert('System Error');
    })
  }

  put() {
    this.userDataService.put(this.user).subscribe(data => {
      if (data) {
        alert('User updated to DB');
        this.user = {};
        this.get();
      } else {
        alert('Error');
      }
    }, error => {
      console.log(error);
      alert('System Error');
    })
  }

  showUserDetails(userDetail) {
    this.showList = false;
    this.user = userDetail;
  }

  deleteUser(user) {
    this.userDataService.delete(user.id).subscribe(data => {
      if (data) {
        alert('User deleted from DB');
        this.user = {};
        this.get();
      } else {
        alert('Error');
      }
    }, error => {
      alert('System Error');
    })
  }


}
