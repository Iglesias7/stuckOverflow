import { Injectable } from "@angular/core";
import { MatTableState } from "../helpers/mattable.state";


@Injectable({ providedIn: 'root' })
export class UserStateService {
   public UserListState = new MatTableState(9);
}