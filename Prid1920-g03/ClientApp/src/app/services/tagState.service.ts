import { Injectable } from "@angular/core";
import { MatTableState } from "../helpers/mattable.state";


@Injectable({ providedIn: 'root' })
export class TagStateService {
   public TagListState = new MatTableState(3);
}