import { Component, OnInit } from "@angular/core";
import { User } from "src/app/models/User";
import { FriendsService } from "src/app/services/friends/friends.service";

@Component({
  selector: "app-friends-list",
  templateUrl: "./friends-list.component.html",
  styleUrls: ["./friends-list.component.css"]
})
export class FriendsListComponent implements OnInit {
  friendsList: User[];

  constructor(private friendsService: FriendsService) {}

  ngOnInit(): void {
    this.getFriendsList();
  }

  public getFriendsList(): void {
    const id = this.friendsService.decodePayloadJWT().unique_name;
    this.friendsService.getFriends(id).subscribe(
      (friendsList: User[]) => (this.friendsList = friendsList),
      err => {
        console.log(err);
      }
    );
  }

  public removeFriend(_id: string): void {
    const id = this.friendsService.decodePayloadJWT().unique_name;
    if (id) {
      this.friendsService.removeFriend(id, _id).subscribe(
        result => {
          console.log(result);
        },
        err => {
          console.log(err);
        }
      );
    }
  }
}
