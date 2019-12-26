import { Injectable } from "@angular/core";
import { MatListPostState } from "../helpers/matListPost.state";


@Injectable({ providedIn: 'root' })
export class StateService {
<<<<<<< HEAD
    public userListState = new MatTableState(6);
=======
    public postListState = new MatListPostState(4);
>>>>>>> f8640cc6f064dfbb67a040403e2ff280130f3c27
}