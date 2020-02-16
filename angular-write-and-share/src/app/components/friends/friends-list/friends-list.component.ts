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
    this.friendsService
      .getFriends(this.friendsService.decodePayloadJWT().unique_name)
      .subscribe(
        (friendsList: User[]) => (this.friendsList = friendsList),
        err => {
          console.log(err);
        }
      );
  }

  public removeFriend(_id: string): void {
    this.friendsService
      .removeFriend(this.friendsService.decodePayloadJWT().unique_name, _id)
      .subscribe(
        result => {
          console.log(result);
        },
        err => {
          console.log(err);
        }
      );
  }
}
