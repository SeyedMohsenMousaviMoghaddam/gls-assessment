<template>
  <q-page class="q-pa-sm">

    <tables-basic></tables-basic>

  </q-page>
</template>

<script>
import {defineComponent, defineAsyncComponent} from 'vue';

export default defineComponent({
  name: "Tables",
  components: {
    TablesBasic: defineAsyncComponent(() => import('components/tables/TableBasic'))
  },
})
</script>

<style>

</style>
