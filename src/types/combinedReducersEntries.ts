import { AuthProps } from "./authProps";
import { AppProps } from "./appProps";

export type CombinedReducersEntries = {
    authReducer: AuthProps
    appReducer: AppProps
}