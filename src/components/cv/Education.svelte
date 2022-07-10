<script lang='ts'>
  import { slide } from 'svelte/transition'

  import type { CurriculumEducationTitleType } from "./Curriculum.type";
  import Toggle from "./Toggle.svelte"

  export let education: CurriculumEducationTitleType[]

  let visible = true
</script>

{#if education && education.length > 0}
<section class="resume-section education-section mb-3">
  <h2 on:click={() => visible = !visible} class="resume-section-title text-uppercase font-weight-bold pb-3 mb-3">Education 
    <Toggle {visible}/>
    <!-- <i class="resume-award-icon fas fa-toggle-on" data-fa-transform="shrink-2"></i> -->
  </h2>
  {#if visible}
  <div class="resume-section-content" transition:slide>
    <ul class="list-unstyled">
      <li class="mb-2">
        {#each education as title}
        <div class="resume-degree font-weight-bold">{ title.degree }</div>
        <div class="resume-degree-org">{ title.organization }</div>
        <div class="resume-degree-time">{ title.time }</div>
        {/each}
      </li>
    </ul>
  </div>
  {/if}
</section><!--//education-section-->
{/if}

<style>
  h2:hover {
    cursor: pointer;
  }
</style>