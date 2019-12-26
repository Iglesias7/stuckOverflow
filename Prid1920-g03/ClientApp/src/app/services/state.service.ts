import { Injectable } from "@angular/core";
import { MatListPostState } from "../helpers/matListPost.state";


@Injectable({ providedIn: 'root' })
export class StateService {
    public postListState = new MatListPostState(4);
}