import { observer } from "mobx-react-lite"

import Browse from "../Movies/Browse/Browse"

export default observer(function Home() {
  return (
    <>
      <Browse />
    </>
  )
})
