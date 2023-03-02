import { observer } from 'mobx-react-lite';
import { Navigate, Outlet, useLocation } from 'react-router-dom';
import { UseStore } from '../Stores/BaseStore';

export default observer(function AuthRoute() {
    const { UserStore } = UseStore();
    const location = useLocation();

    if (!UserStore.IsLogging) {
        return <Navigate to={'/'} state={{ from: location }} />;
    }

    return <Outlet />;
})
