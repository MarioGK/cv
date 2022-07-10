<script type='ts'>
  import { slide } from 'svelte/transition'

  import { mark } from '../../utils/markdown'

  import { toList } from "../../utils/string";

  import Toggle from "./Toggle.svelte"
  import Anchor from "../Anchor.svelte";
  
  import type { CurriculumProjectType } from "./Curriculum.type"
  import CurriculumImage from './CurriculumImage.svelte';

  export let project: CurriculumProjectType
  const technologies = toList(project.technologies)

  const sections = project.sections || []

  let visible = true
</script>

<article class="resume-timeline-item position-relative pb-3">

  <div class="resume-timeline-item-header mb-2">
    <div on:click={() => visible = !visible} class="d-flex flex-column flex-md-row">
      <h3 class="resume-position-title font-weight-bold mb-1">
        <Anchor title={project.title}/>
        <Toggle {visible}/>
      </h3>
      {#if project.company}<div class="resume-company-name ml-auto">{@html mark(project.company) }</div>{/if}
    </div><!--//row-->
    <div class="resume-position-time">{ project.time }</div>
  </div><!--//resume-timeline-item-header-->
  {#if visible}
  <div class="resume-timeline-item-desc" transition:slide>
    {#if project.text.trim() }<p>{@html mark(project.text.trim()) }</p>{/if}
    {#each project.images as img}
      <CurriculumImage {img} />
      <hr />
    {/each}
    {#each sections as section}
      {#if section.title }<h4 class="resume-timeline-item-desc-heading font-weight-bold">{ section.title }:</h4>{/if}
      {#if section.text }<p>{@html mark(section.text) }</p>{/if}
      <ul>
        {#each toList(section.items) as item}
        <li>{@html mark(item.trim()) }</li>
        {/each}
      </ul>
    {/each}
    {#if technologies && technologies.length > 1}
    <h4 class="resume-timeline-item-desc-heading font-weight-bold">Technologies used:</h4>
    <ul class="list-inline">
      {#each technologies as tech}
      <li class="list-inline-item"><span class="badge badge-primary badge-pill">{ tech }</span></li>
      {/each}
    </ul>
    {/if}
  </div><!--//resume-timeline-item-desc-->
  {/if}
</article><!--//resume-timeline-item-->
