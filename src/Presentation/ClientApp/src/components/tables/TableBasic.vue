<template>
  <q-card>
    <q-card-section>
      <div class="text-h6 text-grey-8">
        {{ title }} List
        <q-btn v-if="addbtn" :to="createlink" :label="'Add '+title" class="float-right text-capitalize text-indigo-8 shadow-3" icon="person"/>
      </div>
    </q-card-section>
    <q-card-section class="q-pa-none">
      <q-table
        title=""
        :rows="dataobject"
        :columns="columns"
        row-key="id"
        :filter="filter"
      >
              <template v-slot:body-cell-Action="props">
          <q-td :props="props">
            <q-btn :to="createlink+'/'+props.row.id" v-if="editbtn" icon="edit" size="sm" flat dense/>
            <q-btn  v-if="deletebtn"  @click="confirm(props.row)"  icon="delete" size="sm" class="q-ml-sm" flat dense/>
            <q-btn :to="infolink+'/'+props.row.id" v-if="infobtn" icon="info" size="sm" flat dense/>
          </q-td>
        </template>
        <template v-slot:top-right>
          <q-input v-if="show_filter" filled borderless dense debounce="300" v-model="filter" placeholder="Search">
            <template v-slot:append>
              <q-icon name="search"/>
            </template>
          </q-input>

          <q-btn class="q-ml-sm" icon="filter_list" @click="show_filter=!show_filter" flat/>
        </template>
      </q-table>
    </q-card-section>
  </q-card>
 
</template>

<script>
import {defineComponent, ref} from 'vue'
import { useQuasar } from 'quasar'
import http from "../../http-common";



export default {
  props: {
    addbtn:Boolean,
    editbtn:Boolean,
    deletebtn:Boolean,
    infobtn:Boolean,
    title: String,
    createlink: String,
    infolink: String,
    dataobject: {
        type: Object,
        default: () => ({})
    },
    columns: {
        type: Object,
        default: () => ({})
    }
  },
  name: "TableBasic",
  setup () {
    const $q = useQuasar()
    function deleteItem(id) {
      return http.delete(`/User/Delete/${id}`);
    }
    function alert () {
      $q.dialog({
        dark: true,
        title: 'Alert',
        message: 'Item deleted successfully.'
      }).onOk(() => {
        // console.log('OK')
      }).onCancel(() => {
        // console.log('Cancel')
      }).onDismiss(() => {
        // console.log('I am triggered on both OK and Cancel')
      })
    }
    function confirm (row) {
      $q.dialog({
        dark: true,
        title: 'Confirm',
        message: 'Are you sure you want to permanently delete this item?',
        cancel: true,
        persistent: true
      }).onOk(() => {
         deleteItem(row.id)
            .then(response => {
              alert();
      })
            .catch(e => {
                console.log(e);
        });
      
         
      }).onOk(() => {
        // console.log('>>>> second OK catcher')
      }).onCancel(() => {
         console.log('>>>> Cancel')
      }).onDismiss(() => {
        // console.log('I am triggered on both OK and Cancel')
      })
    }
    return {alert, confirm }

  },
  data() {  
    const show_filter = ref(false);
    return {
      filter: ref(''),
      show_filter      
    };
  }
}
</script>

<style scoped>

</style>
