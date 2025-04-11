<template>
  <div>
    <div v-for="item in todoItems" :key="item.id" class="mb-6">
      <h2 class="font-semibold">
        {{ item.id }}) {{ item.title }} - {{ item.description }} ({{ item.category }}) Completed: {{ item.isCompleted }}
      </h2>
      <div v-for="(progression, index) in item.progressions" :key="index" class="mb-1">
        <div class="text-sm">{{ formatDate(progression.date) }} - {{ getAccumulatedPercent(item.progressions, index) }}%</div>
        <div class="w-full bg-gray-200 rounded h-4">
          <div class="bg-green-500 h-4 rounded" :style="{ width: getAccumulatedPercent(item.progressions, index) + '%' }"></div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import axios from 'axios'

const todoItems = ref([])

onMounted(async () => {
  try {
    const response = await axios.get('https://localhost:44338/Todo')
    console.log('✔ TodoItems recibidos desde API:', response.data) // <- AGREGÁ ESTA LÍNEA
    todoItems.value = response.data
  } catch (error) {
    console.error(' Error al cargar los TodoItems:', error)
  }
})

function getAccumulatedPercent(progressions, index) {
  return progressions.slice(0, index + 1).reduce((sum, p) => sum + p.percent, 0)
}

function formatDate(dateStr) {
  return new Date(dateStr).toLocaleDateString()
}
</script>
